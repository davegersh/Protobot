using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Protobot.Builds.Windows {
    public class WindowsBuildsListSource : MonoBehaviour, IBuildsListSource {
        public event Action<List<BuildData>> OnGetData;

        public void GetData() {
            var saveFilePaths = Directory.EnumerateFiles(WindowsSavingConfig.saveDirectoryPath);

            var buildDatas = new List<BuildData>();

            if (saveFilePaths.Count() > 0) {
                buildDatas = saveFilePaths
                                        .Where(filePath => filePath.Contains(WindowsSavingConfig.saveFileType) && !filePath.Contains(".meta"))
                                        .Select(filePath => {
                                            BinaryFormatter bf = new BinaryFormatter();
                                            FileStream file = File.Open(filePath, FileMode.Open);

                                            BuildData build = (BuildData)bf.Deserialize(file);
                                            file.Close();

                                            return build;
                                        })
                                        .ToList();
            }

            OnGetData?.Invoke(buildDatas);
        }
    }
}