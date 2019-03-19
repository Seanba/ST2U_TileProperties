using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Danger
{
    // Moves and animates Erdrick in 8-bit JRGP style
    public class ErdrickBehavior : MonoBehaviour
    {
        private GridAlignmentMover m_Mover = new GridAlignmentMover { Speed = 48.0f };
        private Animator m_Animator;

        private void Awake()
        {
            m_Animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            // Make sure Erdrick starts off on a proper position
            m_Mover.Direction = Vector2.down;
            m_Mover.Start(gameObject);
        }

        private void LateUpdate()
        {
            // Which way is our character facing?
            m_Animator.SetFloat("Dir_x", m_Mover.Direction.x);
            m_Animator.SetFloat("Dir_y", m_Mover.Direction.y);

            m_Animator.speed = m_Mover.IsMoving ? 1.0f : 0.0f;
        }

        private void Update()
        {
            Vector2 dv = Vector2.zero;
            dv.x = Input.GetAxisRaw("Horizontal");
            dv.y = Input.GetAxisRaw("Vertical");

            m_Mover.Input = dv;
            m_Mover.FrameMove(Time.deltaTime);
        }
    }
}
