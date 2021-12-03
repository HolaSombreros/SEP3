package database.daoservice;

import database.daomodel.NotificationDAO;
import database.daoservice.mapper.NotificationMapper;
import model.MyDateTime;
import model.Notification;

import java.sql.SQLException;
import java.util.List;

public class NotificationDAOService implements NotificationDAO {

    private DatabaseHelper<Notification> databaseHelper;

    public NotificationDAOService(String url, String username, String password) {
        this.databaseHelper = new DatabaseHelper<>(url, username, password);
    }

    @Override public Notification create(int customerId, String text, MyDateTime dateTime, String status) {
        try {
            if (!isNotification(text,dateTime)) {
                List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO notification(message, date_time) VALUES (?,?);", text, dateTime.getLocalDateTime());
                databaseHelper.executeUpdate("INSERT INTO customer_notification(notification_id, customer_id, status) VALUES (?,?,?::notification_status);", keys.get(0), customerId, status);
                return read(keys.get(0), customerId);
            }
            else {
                Notification not = read(text, dateTime);
                databaseHelper.executeUpdate("INSERT INTO customer_notification(notification_id, customer_id, status) VALUES (?,?,?::notification_status);", not.getId(), customerId, status);
                return read(not.getId(), customerId);
            }
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Notification read(int id) {
        try {
            return databaseHelper.mapObject(new NotificationMapper(), "SELECT * FROM notification JOIN customer_notification USING (notification_id) WHERE notification_id = ?", id);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Notification read (int id, int customerId) {
        try {
            return databaseHelper.mapObject(new NotificationMapper(), "SELECT * FROM notification JOIN customer_notification USING (notification_id) WHERE notification_id = ? AND customer_id = ?", id, customerId);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Notification read(String text, MyDateTime time) {
        try {
            return databaseHelper.mapObject(new NotificationMapper(),"SELECT * FROM notification JOIN customer_notification USING (notification_id) WHERE message = ? AND date_time = ?", text, time.getLocalDateTime());
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public boolean isNotification(String text, MyDateTime time) {
        try {
            return databaseHelper.executeQuery(databaseHelper.getConnection(),"SELECT * FROM notification WHERE message = ? AND date_time = ?", text, time.getLocalDateTime()).next();
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public List<Notification> readAll(int customerId, int index) {
        try {
            return databaseHelper.mapList(new NotificationMapper(),
                "SELECT * FROM notification JOIN customer_notification USING (notification_id) WHERE customer_id = ? ORDER BY notification_id DESC LIMIT 5 OFFSET 5 * ?;", customerId, index);
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Notification update(int customerId, Notification notification) {
        try {
            if (read(notification.getId()) == null)
                return create(customerId, notification.getText(), notification.getTime(), notification.getStatus());
            else {
                databaseHelper.executeUpdate("UPDATE notification SET message = ?, date_time = ? WHERE notification_id = ?;", notification.getText(), notification.getTime().getLocalDateTime(), notification.getId());
                databaseHelper.executeUpdate("UPDATE customer_notification SET status = ?::notification_status WHERE notification_id = ? AND customer_id = ?;", notification.getStatus(), notification.getId(), customerId);
                return read(notification.getId(), customerId);
            }
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
