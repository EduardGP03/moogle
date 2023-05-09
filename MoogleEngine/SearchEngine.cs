namespace MoogleEngine;

public class SearchEngine
{
    Library library { get; set; }
    float SmoothValue { get; set; }
    public SearchEngine(string Location,float SmoothValue)
    {
        library = new Library(Location);
        this.SmoothValue = SmoothValue;
    }
    float[] GetQueryVector(string[] query)
    {
        string[] words = Tools.GetWordsWithotRepeat(query);//Esto aparece en el metodo Search y aqui se repite por gusto creo
        float[] idfs = library.WordsIDF(words);
        float[] result = GetTFByWord(words,query);
        for(int i = 0; i < result.Length; i++)
            result[i] = (SmoothValue + (1 - SmoothValue)*result[i])*idfs[i];
        return result;
    }
    float[,] GetDocumentsVector(float[,] tfs, float[] idfs)
    {
        for(int i = 0; i < tfs.GetLength(0); i++)
            for(int j =0; j < tfs.GetLength(1); j++)
                tfs[i,j] *= idfs[j];
        return tfs;
    } 
    float[] GetTFByWord(string[] words_without_repeat,string[] words)
    {
        float MaxTf = 0f;
        float[] result = new float[words_without_repeat.Length];
        for(int i = 0; i < result.Length; i++)
        {
            float repeats = 0;
            for(int j = 0; j < words.Length; j++)
                if(words_without_repeat[i] == words[j])
                    repeats++;
            result[i] = repeats;
            if(repeats > MaxTf)
                MaxTf = repeats;
        }
        for(int i = 0; i < result.Length; i++)
            result[i] /= MaxTf;
        return result;
    }
    float[] MultiplyVectorByMatrix(float[,] matrix, float[] vector)
    {
        float[] scores = new float[matrix.GetLength(0)];
        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            float scalar_product = 0f;
            float v_d = 0f;
            float v_q = 0f;
            for(int j = 0; j < matrix.GetLength(1); j++)
            {
                scalar_product += matrix[i,j] * vector[j];
                v_d += matrix[i,j] * matrix[i,j];
                v_q += vector[j] * vector[j];
            }
            if(v_d * v_q == 0)
                scores[i] = 0;
            else
                scores[i] = scalar_product / (float)Math.Sqrt(v_d * v_q);
        }
        return scores;
    }
    SearchItem[] GetResultsOfQuery(float[] scores,string[] words)
    {
        List<SearchItem> results = new List<SearchItem>();
        for(int i = 0; i < scores.Length; i++)
        {
            if(scores[i] > 0)
                results.Add(new SearchItem(library.Books[i].Title,library.Books[i].GetFragment(words),scores[i]));
        }
        return Tools.Sort(results.ToArray());
    }
    public SearchResult Search(string query)
    {
        string[] Query = Tools.GetWords(query);
        Query = Tools.ClearWhiteSpaces(Query);
        string[] words = Tools.GetWordsWithotRepeat(Query);
        float[] query_vector = GetQueryVector(Query);
        float[,] tfs = library.WordsTF(words);
        float[,] documents_vectors = GetDocumentsVector(tfs,library.WordsIDF(words));
        float[] scores = MultiplyVectorByMatrix(documents_vectors,query_vector);
        SearchItem[] items_result = GetResultsOfQuery(scores,words);
        return new SearchResult(items_result);
                     
    }
}