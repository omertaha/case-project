using System.Collections.Generic;
using Characters;

/// <summary>
/// Finding the place of the characters.
/// </summary>
namespace Managers
{
    public class RankingManager : Manager<RankingManager>
    {
        private List<Character> _characters;

        protected override void Awake()
        {
            base.Awake();
            _characters = new List<Character>();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Ready?.Invoke();
        }

        public void RegisterCharacter(Character character)
        {
            if (!_characters.Contains(character))
                _characters.Add(character);
        }


        public List<Character> GetRankedCharacters()
        {
            _characters.Sort((a, b) => b.Progress.CompareTo(a.Progress));
            return _characters;
        }

        public int GetPlayerRank(Character player)
        {
            var rankedList = GetRankedCharacters();
            return rankedList.IndexOf(player) + 1; // +1 for 1-based rank
        }
    }
}
