package model.enums;

public enum Category {
    BOOK("Book"),
    MUSIC("Music"),
    HOME("Home"),
    FOOD("Food"),
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
