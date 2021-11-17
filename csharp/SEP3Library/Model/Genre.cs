using System.ComponentModel;

namespace SEP3Library.Model {
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