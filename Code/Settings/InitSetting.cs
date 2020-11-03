﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    public class InitSetting
    {
        private static object syncRoot = new Object();
        private static volatile InitSetting instance;
        public static InitSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new InitSetting();
                        }
                    }
                }
                return instance;
            }
        }
        public IDictionary<int, string> HashTypes { get; private set; }
        public void InitHashTypes(IDictionary<int, string> HashTypes)
        {
            this.HashTypes = HashTypes;
        }
        public int MaxNumberOfBytesInSalt { get; private set; }
        public void InitMaxNumberOfBytesInSalt(int maxLengthOfHashSaltBase64)
        {
            MaxNumberOfBytesInSalt = (int)(3 * maxLengthOfHashSaltBase64 / 4 - 1.75);
        }
    }
}
