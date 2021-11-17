package model.enums;

import com.google.gson.annotations.SerializedName;

public enum ItemStatus {
    @SerializedName("OutOfStock")
    OUTOFSTOCK ("Out of Stock"),
    @SerializedName("InStock")
    INSTOCK ("In Stock");

    private String  status;
    private  ItemStatus(String status) {
        this.status = status;
    }

    public String toString(){
        return status;
    }

    public static ItemStatus fromString(String value) {
        for (ItemStatus option : ItemStatus.values()) {
            if (option.status.equalsIgnoreCase(value)) {
                return option;
            }
        }
        return null;
    }


}
