using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Golf
{
    public class LevelContr : MonoBehaviour
    {
        public Spawners spawner;
        public float delay = 0.5f;
        private float m_lastSpawnerTime = 0;

        public float delayMax = 2f;
        public float delayMin = 0f;
        public float delayStep = 0.1f;

        public int score = 0;

        public int hightScore = 0;
        private float m_delay = 0.5f;

        private List<GameObject> m_stones = new List<GameObject>(16);

        public void ClearStone()
        {
            foreach (var stone in m_stones)
            { 
                Destroy(stone);
            }
            m_stones.Clear();
        }

        public void Start()
        {
            //StartCoroutine(SpawnStone());
            m_lastSpawnerTime = Time.time;
            RefreshDelay();
        }


        private void OnEnable()
        {
            GameEvents.onCollisionStones += GameOver;
            GameEvents.onStickHit += OnStickHit;
            score = 0;

        }

        private void OnDisable()
        {
            GameEvents.onCollisionStones -= GameOver;
            GameEvents.onStickHit -= OnStickHit;
        }

        private void OnStickHit()
        {
            score++;
            hightScore = Mathf.Max(hightScore, score);

            Debug.Log($"score: {score} - hightScore: {hightScore}");
        }


        private void Update()
        {
            if (Time.time >= m_lastSpawnerTime + m_delay)
            {
                var stone = spawner.Spawn();
                m_stones.Add(stone);
                m_lastSpawnerTime = Time.time;

                RefreshDelay();
            }
        }
        public void RefreshDelay()
        { 
            m_delay = UnityEngine.Random.Range(delayMin, delayMax);
            delayMax = Mathf.Max(delayMin, delayMax - delayStep);
        }

        private void GameOver()
        {
            enabled = false;
        }

        IEnumerator WaitEvent(System.Action callback)
        {
            yield return new WaitForSeconds(delayStep);
            callback?.Invoke();
        }
    }
}