package model.enums;

public enum Language {
    ENGLISH("English"),
    DANISH("Danish");

    private String language;

    Language(String language) {
        this.language = language;
    }

    public String toString(){
        return language;
    }

    public static Language fromString(String value) {
        for (Language option : Language.values()) {
            if (option.language.equalsIgnoreCase(value)) {
                return option;
            }
        }
        return null;
    }
}
