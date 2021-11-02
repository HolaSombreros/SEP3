package database.model.enums;

public enum OrderStatus {
    PENDING("Pending"),
    FINISHED("Finished"),
    CANCELLED("Cancelled");

    private String status;

    OrderStatus(String status) {
        this.status = status;
    }

    public String toString(){
        return status;
    }

    public static OrderStatus fromString(String value) {
        for (OrderStatus option : OrderStatus.values()) {
            if (option.status.equalsIgnoreCase(value)) {
                return option;
            }
        }
        return null;
    }
}
