package database.daoservice.mapper;

import model.Category;
import model.Item;
import model.enums.ItemStatus;

import java.sql.ResultSet;
import java.sql.SQLException;

public class ItemMapper implements DataMapper<Item> {
    @Override
    public Item map(ResultSet resultSet) throws SQLException {
        return new Item(resultSet.getInt("item_id"),resultSet.getString("item_name"),resultSet.getString("description"),
                resultSet.getDouble("price"), new Category(resultSet.getInt("category_id"), resultSet.getString("category_name")),resultSet.getInt("quantity"),
                ItemStatus.fromString(resultSet.getString("status")), resultSet.getInt("discount"), resultSet.getString("image_filepath"));

    }
}
