package model.enums;

import com.google.gson.annotations.SerializedName;

public enum Genre {

    @SerializedName("Action")
    ACTION("Action"),
    @SerializedName("Romance")
    ROMANCE("Romance"),
    @SerializedName("History")
    HISTORY("History"),
    @SerializedName("Crime")
    CRIME("Crime"),
    @SerializedName("Fantasy")
    FANTASY("Fantasy"),
    @SerializedName("Horror")
    HORROR("Horror"),
    @SerializedName("Classic")
    CLASSIC("Classic"),
    @SerializedName("Manga")
    MANGA("Manga"),
    @SerializedName("LightNovel")
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
