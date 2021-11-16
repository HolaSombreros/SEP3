package model.enums;

import com.google.gson.annotations.SerializedName;

public enum OrderStatus {
    @SerializedName("Pending")
    PENDING("Pending"),
    @SerializedName("Finished")
    FINISHED("Finished"),
    @SerializedName("Cancelled")
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
