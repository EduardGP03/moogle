namespace MoogleEngine;


public class Moogle
{
    SearchEngine engine;
    public Moogle()
    {
        engine = new SearchEngine("/home/eduard/moogle/Content",0.4f);
    }
    public SearchResult Query(string query) {
        return engine.Search(query);
    }
}