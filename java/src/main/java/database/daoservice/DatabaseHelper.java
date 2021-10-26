package database.daoservice;

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

    private static PreparedStatement preparedStatement(Connection connection, String query, Object... parameters) throws SQLException {
        PreparedStatement stat = connection.prepareStatement(query);
        for(int i = 0; i < parameters.length; i++) {
            stat.setObject(i + 1, parameters[i]);
        }
        return stat;
    }

    private static PreparedStatement preparedStatementWithKeys(Connection connection, String query, Object... parameters) throws SQLException {
        PreparedStatement statement = connection.prepareStatement(query,PreparedStatement.RETURN_GENERATED_KEYS);
        for(int i = 0; i < parameters.length; i++) {
            statement.setObject(i + 1, parameters[i]);
        }
        return statement;
    }


    public ResultSet executeQuery(Connection connection, String query,Object... parameters ) throws SQLException{
        PreparedStatement statement = preparedStatement(connection,query,parameters);
        return statement.executeQuery();
    }

    public void executeUpdate(String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            PreparedStatement statement = preparedStatement(connection,query,parameters);
            statement.executeUpdate();
        }
        catch (SQLException e){
            throw new IllegalStateException(e.getMessage());
        }
    }

    public void executeUpdateWithKeys(String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            PreparedStatement statement = preparedStatementWithKeys(connection,query,parameters);
            statement.executeUpdate();
        }
        catch (SQLException e){
            throw new IllegalStateException(e.getMessage());
        }
    }

    public T mapObject(DataMapper<T> mapper, String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            ResultSet resultSet = executeQuery(connection,query,parameters);
            if(resultSet.next()){
                mapper.create(resultSet);
            }
            else{
                return null;
            }
        } catch (SQLException e){
            throw new IllegalStateException(e.getMessage());
        }
        return null;
    }


    public T mapLists(DataMapper<T> mapper, String query, Object... parameters) throws SQLException{
        try(Connection connection = getConnection()){
            ResultSet resultSet = executeQuery(connection,query,parameters);
            List<T> itemList = new ArrayList<>();
            while(resultSet.next()){
                itemList.add(mapper.create(resultSet));
            }
        } catch (SQLException e){
            throw new IllegalStateException(e.getMessage());
        }
        return null;
    }

}
