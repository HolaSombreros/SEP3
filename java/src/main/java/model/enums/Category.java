package model.enums;

import com.google.gson.annotations.SerializedName;

public enum Category {
    @SerializedName("Book")
    BOOK("Book"),
    @SerializedName("Music")
    MUSIC("Music"),
    @SerializedName("Home")
    HOME("Home"),
    @SerializedName("Food")
    FOOD("Food"),
    @SerializedName("Games")
    GAMES("Games");

    private String category;

    Category(String category) {
        this.category = category;
    }

    @Override
    public String toString() {
        return category;
    }

    public static Category fromString(String value) {
        for (Category option : Category.values()) {
            if (option.category.equalsIgnoreCase(value)) {
                return option;
            }
        }
        return null;
    }
}
