package database.daoservice;

import database.daoservice.mapper.DataMapper;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class DatabaseHelper<T> {
    private String url;
    private String username;
    private String password;

    public DatabaseHelper(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
    }

    public Connection getConnection()throws SQLException{
        if(username == null){
            return DriverManager.getConnection(url);
        }
        else{
            return DriverManager.getConnection(url,username,password);
        }
    }
    /**
     * Creates a PreparedStatement object using the query and the parameters given as arguments
     * @param connection
     * @param query
     * @param parameters
     * @return
     * @throws SQLException
     */
    private static PreparedStatement preparedStatement(Connection connection, String query, Object... parameters) throws SQLException {
        PreparedStatement statement = connection.prepareStatement(query);
        for(int i = 0; i < parameters.length; i++) {
            statement.setObject(i + 1, parameters[i]);
        }
        return statement;
    }

    /**
     * Creates a PreparedStatement object using the query and the parameters given as arguments
     * Configures the statement to return the generated keys
     * @param connection
     * @param query
     * @param parameters
     * @return
     * @throws SQLException
     */
    private static PreparedStatement preparedStatementWithKeys(Connection connection, String query, Object... parameters) throws SQLException {
        PreparedStatement statement = connection.prepareStatement(query,PreparedStatement.RETURN_GENERATED_KEYS);
        for(int i = 0; i < parameters.length; i++) {
            statement.setObject(i + 1, parameters[i]);
        }
        return statement;
    }


    /**
     * Executes the query given as an argument and returns the generated result set
     * @param connection
     * @param query
     * @param parameters
     * @return
     * @throws SQLException
     */
    public ResultSet executeQuery(Connection connection, String query,Object... parameters ) throws SQLException{
        PreparedStatement statement = preparedStatement(connection,query,parameters);
        return statement.executeQuery();
    }

    public ResultSet executeQuery(String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            PreparedStatement statement = preparedStatement(connection,query,parameters);
            return statement.executeQuery();
        }
    }

    /**
     * Opens a connection to the database and executes the query given as an argument
     * @param query
     * @param parameters
     * @throws SQLException
     */
    public void executeUpdate(String query, Object... parameters) throws SQLException{
        Connection connection = getConnection();
        try{
            connection.setAutoCommit(false);
            PreparedStatement statement = preparedStatement(connection,query,parameters);
            statement.executeUpdate();
            connection.commit();
        }
        catch (SQLException e){
            connection.rollback();
            throw new IllegalArgumentException(e.getMessage());
        }
        finally {
            connection.close();
        }
    }

    /**
     * Opens a connection to the database
     * Executes the query given as a argument and returns the list of keys generated by the query
     * @param query
     * @param parameters
     * @return
     * @throws SQLException
     */
    public List<Integer> executeUpdateWithKeys(String query, Object... parameters) throws SQLException{
        Connection connection = getConnection();
        try{
            connection.setAutoCommit(false);
            PreparedStatement statement = preparedStatementWithKeys(connection,query,parameters);
            statement.executeUpdate();
            connection.commit();
            List<Integer> keys = new ArrayList<>();
            ResultSet resultSet = statement.getGeneratedKeys();
            while(resultSet.next()) {
                keys.add(resultSet.getInt(1));
            }
            return keys;
        }
        catch (SQLException e){
            connection.rollback();
            throw new IllegalArgumentException(e.getMessage());
        }
        finally {
            connection.close();
        }
    }


    /**
     * Opens a connection to the database,
     * executes the query given as a parameter
     * returns a mapped object according to the declared type
     *
     *
     * @param mapper
     * @param query - is a prepared statement
     * @param parameters - are used for the required values within the prepared statement
     * @return
     * @throws SQLException
     */
    public T mapObject(DataMapper<T> mapper, String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            ResultSet resultSet = executeQuery(connection,query,parameters);
            if(resultSet.next()){
                return mapper.map(resultSet);
            }
            else{
                return null;
            }
        } catch (SQLException e){
            throw new IllegalStateException(e.getMessage());
        }
    }

    /**
     * Opens a connection to the database
     * executes the query given as an argument
     * returns a mapped list of the declared object type
     * @param mapper
     * @param query
     * @param parameters
     * @return
     * @throws SQLException
     */
    public List<T> mapList(DataMapper<T> mapper, String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            ResultSet resultSet = executeQuery(connection,query,parameters);
            List<T> itemList = new ArrayList<>();
            while(resultSet.next()){
                itemList.add(mapper.map(resultSet));
            }
            return itemList;
        } catch (SQLException e){
            throw new IllegalStateException(e.getMessage());
        }
    }
}
