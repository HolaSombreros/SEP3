using System.ComponentModel;

namespace SEP3Library.Models {
    public enum Genre {
        Action,
        Romance,
        History,
        Crime, 
        Fantasy,
        Horror,
        Classic,
        Manga,
        [Description("Light novel")]
        Lightnovel
    }
}