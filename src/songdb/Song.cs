namespace songdb;
public class Song {
    public Song(string name, string album, string artist, string lyrics, string mp3)
    {
        Name = name;
        Album = album;
        Artist = artist;
        Lyrics = lyrics;
        Mp3 = mp3;
    }

    public Song(string name, string album, string lyrics)
    {
        Name = name;
        Album = album;
        Lyrics = lyrics;
    }

    public string Name { get; set; }
    public string Album { get; set; }
    public string Lyrics { get; set; }
    public string Artist { get; set;}
    public string Mp3 { get; set; }
}