package database.daomodel;

import model.MyDateTime;
import model.Notification;

import java.util.List;

public interface NotificationDAO {
    Notification create(int customerId, String text, MyDateTime dateTime, String status);
    Notification read(int id);
    List<Notification> readAll(int customerId, int index);
    Notification update(int customerId, Notification notification);
}
