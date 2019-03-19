using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Danger
{
    // Helper class to move with grid alignment
    // This is like how a character would move around in a classical top-down JRPG
    public class GridAlignmentMover
    {
        private GameObject m_GameObject;
        private float m_Timer;
        private Vector2 m_Input;

        public int Alignment { get; set; } = 16;
        public float Speed { get; set; } = 64.0f;
        public Vector2 Direction { get; set; }

        public Vector2 Input
        {
            get => m_Input;
            set
            {
                if (value != m_Input)
                {
                    m_Input = value;

                    // Only allow movement up, down, left, or right (no diagonals)
                    // Prefer left/right movement
                    if (m_Input.x != 0)
                    {
                        m_Input.y = 0;
                    }
                }
            }
        }

        public bool IsMoving => (m_Timer > 0) || Input != Vector2.zero;

        public void Start(GameObject go)
        {
            m_Timer = 0.0f;
            m_GameObject = go;
            AlignPosition();
        }

        public void FrameMove(float dt)
        {
            if (m_Timer == 0)
            {
                if (Input == Vector2.zero)
                {
                    // Not moving, no input
                    return;
                }

                Direction = Input;
            }

            float timeToMove = Alignment / Speed;
            var prevTime = m_Timer;
            m_Timer += dt;

            if (m_Timer < timeToMove)
            {
                Translate(dt);
            }
            else
            {
                dt = timeToMove - prevTime;
                m_Timer -= timeToMove;

                // Partial move to finish movement to grid
                Translate(dt);

                // Use up the rest the time to start moving to the next grid alignment provided by input
                if (Input != Vector2.zero)
                {
                    Direction = Input;
                    Direction.Normalize();
                    Translate(m_Timer);
                }
                else
                {
                    // Stay in place until we get more input
                    m_Timer = 0.0f;
                    AlignPosition();
                }
            }
        }

        private void Translate(float dt)
        {
            var dv = Direction * Speed * dt;
            m_GameObject.transform.Translate(dv);
        }

        private void AlignPosition()
        {
            var pos = m_GameObject.transform.position;
            pos.x = AlignValue(pos.x);
            pos.y = AlignValue(pos.y);
            m_GameObject.transform.position = pos;
        }

        private int AlignValue(float value)
        {
            if (value < 0)
            {
                return (int)(((value - Alignment * 0.5f) / Alignment)) * Alignment;
            }

            return (int)(((value + Alignment * 0.5f) / Alignment)) * Alignment;
        }
    }
}
