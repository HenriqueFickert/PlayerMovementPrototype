using System.Linq;
using UnityEngine;

namespace StatePattern
{
    public class AgentAnimation : MonoBehaviour
    {
        private Animator animator;
        private AnimationClip[] clips;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            clips = animator.runtimeAnimatorController.animationClips;
        }
        
        public void PlayAnimation(EAgentState agentState)
        {
            string agentAnimation = agentState.ToString();
            if (!HasAnimation(agentAnimation))
                return;

            Play(agentAnimation);
        }

        public void Play(string state)
        {
            animator.Play(state, -1, 0f);
        }

        public bool IsInAnimation(EAgentState agentState) => animator.GetCurrentAnimatorStateInfo(0).IsName(agentState.ToString());

        private bool HasAnimation(string name) => clips.Any(clip => clip.name == name);
    }
}