using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalShooter
{
    public class SequenceManager : MonoBehaviour
    {
        [Header("Preload")]
        [Tooltip("사전에 먼저 로드시킬 프리팹 입니다. 프리팹 뿐만 아니라 사운드, 텍스쳐, 기타 등등 적용 가능합니다.")]
        [SerializeField] private GameObject[] m_PreloadedAssets;

        [Space(10)]
        [SerializeField] private bool m_Debug;
    }
}
