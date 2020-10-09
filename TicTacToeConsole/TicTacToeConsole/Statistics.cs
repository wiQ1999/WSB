using System;
using System.Collections.Generic;
using System.IO;

namespace TicTacToeConsole
{
    class Statistics
    {
        public string FileName { get; }


        public Statistics()
        {
            FileName = "Statistics.txt";
        }


        private List<string> PlayerList(StreamReader a_sr)
        {
            List<string> _oResult = new List<string>();

            bool _bIsFirstLine = true;
            string _sLine;
            while ((_sLine = a_sr.ReadLine()) != null)
            {
                if(_bIsFirstLine)
                {
                    _bIsFirstLine = false;
                    continue;
                }

                HeaderElements _Elements = GetHeaderElements(_sLine);

                if (!_oResult.Contains(_Elements.Winner))
                    _oResult.Add(_Elements.Winner);
                if (!_oResult.Contains(_Elements.Loser))
                    _oResult.Add(_Elements.Loser);
            }

            return _oResult;
        }

        private HeaderElements GetHeaderElements(string a_sLine)
        {
            HeaderElements _Result = new HeaderElements();
            string _sTemp = string.Empty;
            int _iSwitcher = 0;

            for (int i = 0; i < a_sLine.Length; i++)
            {
                if (a_sLine[i] == '.')
                {
                    switch (_iSwitcher)
                    {
                        case 0:
                            _Result.Lp = int.Parse(_sTemp);
                            break;
                        case 1:
                            _Result.Winner = _sTemp;
                            break;
                        case 2:
                            _Result.Loser = _sTemp;
                            break;
                    }

                    _sTemp = string.Empty;
                    _iSwitcher++;
                    continue;
                }

                _sTemp += a_sLine[i];
            }

            return _Result;
        }

        private int NextIndex(StreamReader a_sr)
        {
            int _iResult = 0;
            while (a_sr.ReadLine() != null)
                _iResult++;

            return _iResult++;
        }

        public object Read(StatsOptions a_Action)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    switch (a_Action)
                    {
                        case StatsOptions.NextIndex:
                            return NextIndex(sr);
                        case StatsOptions.PlayersList:
                            return PlayerList(sr);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public void SaveGame(string a_Winner, string a_Loser)
        {
            //zapis w przypadku gdy plik jeszcze nie istnieje
            bool _bIsFileExist = true;
            string _sFirstLine = string.Empty;
            int _iNextIndex = 0;
            if (!File.Exists(FileName))
            {
                _bIsFileExist = false;
                _sFirstLine = $"[LP].[Winner].[Loser]. Po każdej informacji jest konieczna kropka!\n1.{a_Winner}.{a_Loser}.";
            }
            else
                _iNextIndex = (int)Read(StatsOptions.NextIndex);

            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                if (!_bIsFileExist)//jeżeli plik nie istniał zapisuje nagłówek wraz z rekordem
                    sw.WriteLine(_sFirstLine);
                else//zapis rekordu do pliku
                    sw.WriteLine($"{_iNextIndex}.{a_Winner}.{a_Loser}.");
            }
        }
    }
}
