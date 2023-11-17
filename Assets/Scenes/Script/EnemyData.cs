using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
        public float moveSpeed = 5f;
        public int health = 100;
}
