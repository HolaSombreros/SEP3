package database.daoservice.mapper;

import database.daoservice.DataMapper;
import database.model.Item;
import database.model.enums.Category;
import database.model.enums.ItemStatus;

import java.sql.ResultSet;
import java.sql.SQLException;

public class ItemMapper implements DataMapper<Item> {
    @Override
    public Item map(ResultSet resultSet) throws SQLException {
        return new Item(resultSet.getInt("item_id"),resultSet.getString("name"),resultSet.getString("description"),
                resultSet.getDouble("price"), Category.fromString(resultSet.getString("category")),resultSet.getInt("quantity"),
                ItemStatus.fromString(resultSet.getString("status")), resultSet.getInt("discount"));

    }
}
