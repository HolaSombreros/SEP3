package model.enums;

public enum Genre {

    ACTION("Action"),
    ROMANCE("Romance"),
    HISTORY("History"),
    CRIME("Crime"),
    FANTASY("Fantasy"),
    HORROR("Horror"),
    CLASSIC("Classic"),
    MANGA("Manga"),
    LIGHTNOVEL("LightNovel");

    private String genre;

    Genre(String genre) {
        this.genre = genre;
    }

    public String toString(){
        return genre;
    }

    public static Genre fromString(String value) {
        for (Genre option : Genre.values()) {
            if (option.genre.equalsIgnoreCase(value)) {
                return option;
            }
        }
        return null;
    }
}
