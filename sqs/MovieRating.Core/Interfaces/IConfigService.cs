namespace MovieRating.Core.Interfaces;

public interface IConfigService
{
    string GetApiValue(string environmentNameVariable);
    string GetDbValue(string environmentNameVariable);
}