package database.daoservice.mapper;

import model.MyDateTime;
import model.Notification;

import java.sql.ResultSet;
import java.sql.SQLException;

public class NotificationMapper implements DataMapper<Notification> {
    @Override public Notification map(ResultSet resultSet) throws SQLException {
        return new Notification(resultSet.getInt("notification_id"), resultSet.getString("text"), new MyDateTime(resultSet.getTimestamp("date_time").toLocalDateTime()),
            resultSet.getString("status"));
    }
}
