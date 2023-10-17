using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class CharacterManager : Service
    {
        [field: SerializeField] public List<Scientist> AllScientists { get; private set; } = new List<Scientist>();

        public override void Init()
        {
            if (IsInit)
                return;
            IsInit = true;
            
            var _allScientists = FindObjectsOfType<Scientist>();

            foreach (var _security in _allScientists)
                AllScientists.Add(_security);
        }

        public void OnScientistDeadHandler(Scientist _scientist)
        {
            AllScientists.Remove(_scientist);
        }

        public void OnScientistSaved(Scientist _scientist)
        {
            var _allSaved = true;

            foreach (var _scien in AllScientists)
            {
                if(_scien.IsDead)
                    continue;

                if (!_scien.IsSafe)
                {
                    _allSaved = false;
                    break;
                }
            }

            if (_allSaved)
            {
                PlayerManager.MissionComplete = true;
                ServiceLocator.Instance.Get<UIManager>().Get<SummaryGUI>().Open();
            }
        }
    }
}
