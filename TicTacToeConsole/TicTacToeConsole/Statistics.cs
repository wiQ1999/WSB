using System;
using System.Collections.Generic;
using System.IO;

namespace TicTacToeConsole
{
    class Statistics
    {
        /// <summary>
        /// File with statistics
        /// </summary>
        public string FileName { get; }


        public Statistics()
        {
            FileName = "Statistics.txt";
        }


        /// <summary>
        /// Search for statistics about player
        /// </summary>
        /// <param name="a_sFirstPlayerName">Player to find</param>
        /// <param name="a_sSecondPlayerName">The player with whom the matches were played</param>
        /// <returns>Statistics about player</returns>
        public PlayerStats ReadPlayerStats(string a_sFirstPlayerName, string a_sSecondPlayerName)
        {
            PlayerStats _Result = new PlayerStats();

            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    _Result.Name = a_sFirstPlayerName;

                    bool _bIsFirstLine = true;
                    string _sLine;
                    while ((_sLine = sr.ReadLine()) != null)
                    {
                        if (_bIsFirstLine)
                        {
                            _bIsFirstLine = false;
                            continue;
                        }

                        HeaderElements _Elements = GetHeaderElements(_sLine);

                        if((_Elements.PlayerKOLKO == a_sFirstPlayerName && _Elements.PlayerKRZYZYK == a_sSecondPlayerName) || (_Elements.PlayerKOLKO == a_sSecondPlayerName && _Elements.PlayerKRZYZYK == a_sFirstPlayerName))
						{
                            if (_Elements.Winner == a_sFirstPlayerName)
                                _Result.Wins++;
                            else if (_Elements.Winner == " ")
                                _Result.Draw++;
                            else
                                _Result.Loses++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return _Result;
        }

        /// <summary>
        /// Search for statistics about player
        /// </summary>
        /// <param name="a_sPlayerName">Player to find</param>
        /// <returns>Statistics about player</returns>
        public PlayerStats ReadPlayerStats(string a_sPlayerName)
		{
            PlayerStats _Result = new PlayerStats();

            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    _Result.Name = a_sPlayerName;

                    bool _bIsFirstLine = true;
                    string _sLine;
                    while ((_sLine = sr.ReadLine()) != null)
                    {
                        if (_bIsFirstLine)
                        {
                            _bIsFirstLine = false;
                            continue;
                        }

                        HeaderElements _Elements = GetHeaderElements(_sLine);

                        if (_Elements.PlayerKOLKO == a_sPlayerName || _Elements.PlayerKRZYZYK == a_sPlayerName)
						{
                            if (_Elements.Winner == a_sPlayerName)
                                _Result.Wins++;
                            else if (_Elements.Winner == " ")
                                _Result.Draw++;
                            else
                                _Result.Loses++;
						}
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return _Result;
		}

        /// <summary>
        /// Get list of players from statistics file
        /// </summary>
        /// <returns>Players list</returns>
        public List<string> ReadPlayerList()
        {
            List<string> _oResult = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    bool _bIsFirstLine = true;
                    string _sLine;
                    while ((_sLine = sr.ReadLine()) != null)
                    {
                        if(_bIsFirstLine)
                        {
                            _bIsFirstLine = false;
                            continue;
                        }

                        HeaderElements _Elements = GetHeaderElements(_sLine);

                        if (!_oResult.Contains(_Elements.PlayerKOLKO))
                            _oResult.Add(_Elements.PlayerKOLKO);
                        if (!_oResult.Contains(_Elements.PlayerKRZYZYK))
                            _oResult.Add(_Elements.PlayerKRZYZYK);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return _oResult;
        }

        /// <summary>
        /// Counts lines in txt file
        /// </summary>
        /// <returns>Next index in statistics file</returns>
        public int ReadNextIndex()
        {
            int _iResult = 0;
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    while (sr.ReadLine() != null)
                        _iResult++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return _iResult++;
        }

        /// <summary>
        /// Get main information from txt file
        /// </summary>
        /// <param name="a_sLine">Line of text file</param>
        /// <returns>Main informations about winner and players</returns>
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
                            _Result.PlayerKOLKO = _sTemp;
                            break;
                        case 3:
                            _Result.PlayerKRZYZYK = _sTemp;
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

        /// <summary>
        /// Save information about end of the game
        /// </summary>
        /// <param name="a_sKOLKO">Player KOLKO</param>
        /// <param name="a_sKRZYZYK">Player KRZYZYK</param>
        /// <param name="a_Winner">Winner of the match</param>
        public void SaveGame(string a_sKOLKO, string a_sKRZYZYK, Character a_Winner)
        {
            //zapis w przypadku gdy plik jeszcze nie istnieje
            bool _bIsFileExist = true;
            string _sFirstLine = string.Empty;
            int _iNextIndex = 1;
            if (!File.Exists(FileName))
            {
                _bIsFileExist = false;
                _sFirstLine = $"[LP].[Winner].[PlayerKOLKO].[PlayerKRZYZYK]. Po każdej informacji jest konieczna kropka!";
            }
            else
                _iNextIndex = ReadNextIndex();

            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                if (!_bIsFileExist)//jeżeli plik nie istniał zapisuje nagłówek wraz z rekordem
                    sw.WriteLine(_sFirstLine);

                //okreslenie zwycięscy - puste oznacza remis
                string _sWinner = string.Empty;
				switch (a_Winner)
				{
					case Character.KOLKO:
                        _sWinner = a_sKOLKO;
                        break;
					case Character.KRZYZYK:
                        _sWinner = a_sKRZYZYK;
                        break;
					case Character.EMPTY:
                        _sWinner = " ";
                        break;
				}

                sw.WriteLine($"{_iNextIndex}.{_sWinner}.{a_sKOLKO}.{a_sKRZYZYK}.");
            }
        }
    }
}
