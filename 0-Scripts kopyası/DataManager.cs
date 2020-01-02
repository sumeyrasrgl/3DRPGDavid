using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour {
    public static DataManager dataManager;

    private void Awake() {
        if (DataManager.dataManager==null) {
            DataManager.dataManager=this;
        } else {
            if (this!=DataManager.dataManager) {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(DataManager.dataManager.gameObject);
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/saveGame.woa4");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        Quaternion playerRotation = player.transform.rotation;
        bf.Serialize(file, playerPosition.x);
        bf.Serialize(file, playerPosition.y);
        bf.Serialize(file, playerPosition.z);
        bf.Serialize(file, playerRotation.x);
        bf.Serialize(file, playerRotation.y);
        bf.Serialize(file, playerRotation.z);
        bf.Serialize(file, playerRotation.w);

        file.Close();

        Debug.Log("Veriler kaydedildi");
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath+"/saveGame.woa4")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/saveGame.woa4", FileMode.Open);

            float posX = (float) bf.Deserialize(file);
            float posY = (float) bf.Deserialize(file);
            float posZ = (float) bf.Deserialize(file);
            float quaX = (float) bf.Deserialize(file);
            float quaY = (float) bf.Deserialize(file);
            float quaZ = (float) bf.Deserialize(file);
            float quaW = (float) bf.Deserialize(file);

            Vector3 savedPosition = new Vector3(posX,posY,posZ);
            Quaternion savedRotation = new Quaternion(quaX,quaY,quaZ,quaW);

            file.Close();

            Debug.Log("Kayitlar yuklendi");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = savedPosition;
            player.transform.rotation = savedRotation;
        }
    }

}
