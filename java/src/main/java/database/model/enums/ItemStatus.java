package database.model.enums;

public enum ItemStatus {
    OUTOFSTOCK ("Out of Stock"),
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
