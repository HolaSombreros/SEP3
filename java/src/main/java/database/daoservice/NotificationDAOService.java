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
            List<Integer> keys = databaseHelper.executeUpdateWithKeys("INSERT INTO notification(text, date_time, customer_id, status) VALUES (?,?,?,?::notification_status);", text,
                dateTime.getLocalDateTime(), customerId, status);
            return read(keys.get(0));
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public Notification read(int id) {
        try {
            Notification notification = databaseHelper.mapObject(new NotificationMapper(), "SELECT * FROM notification WHERE notification_id = ?", id);
            return notification;
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }

    @Override public List<Notification> readAll(int customerId, int index) {
        try {
            List<Notification> notifications = databaseHelper.mapList(new NotificationMapper(),
                "SELECT * FROM notification WHERE customer_id = ? ORDER BY notification_id DESC LIMIT 5 OFFSET 5 * ?", customerId, index);
            return notifications;
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
                databaseHelper.executeUpdate("UPDATE notification SET text = ?, date_time = ?, status = ?::notification_status, customer_id = ? WHERE notification_id = ?;", notification.getText(),
                    notification.getTime().getLocalDateTime(), notification.getStatus(), customerId, notification.getId());
                return read(notification.getId());
            }
        }
        catch (SQLException e) {
            throw new IllegalArgumentException(e.getMessage());
        }
    }
}
