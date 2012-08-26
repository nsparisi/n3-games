using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CyborgPunch.Core
{
    public class Transform : Component
    {
        private List<Transform> children;
        private HashSet<int> childrenHash;

        private Transform parent;
        public Transform Parent
        {
            get { return this.parent; }
            set
            {
                if (value == this)
                    return;

                //handle previous parent
                if (this.parent != null)
                {
                    this.parent.RemoveChild(this);
                }

                //setup new parent relationship
                this.parent = value;
                if (this.parent != null)
                {
                    this.parent.AddChild(this);
                }


                DetermineLocalPosition();
            }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
            set { 
                this.position = value;
                DetermineLocalPosition();
            }
        }

        private Vector2 localPosition;
        public Vector2 LocalPosition
        {
            get { return this.localPosition; }
            set 
            { 
                this.localPosition = value;
                DeterminePosition();
            }
        }

        private void AddChild(Transform child)
        {
            int id = child.blob.ID;
            if (childrenHash.Add(id))
            {
                children.Add(child);
                child.DetermineLocalPosition();
            }
        }

        private void RemoveChild(Transform child)
        {
            int id = child.blob.ID;
            if (childrenHash.Contains(id))
            {
                childrenHash.Remove(id);
                children.Remove(child);
            }
        }

        void DeterminePosition()
        {
            position = localPosition;
            Transform nextParent = parent;
            while (nextParent != null)
            {
                position += nextParent.localPosition;
                nextParent = nextParent.parent;
            }

            RefreshChildren();
        }

        void DetermineLocalPosition()
        {
            localPosition = position;
            Transform nextParent = parent;
            while (nextParent != null)
            {
                localPosition -= nextParent.localPosition;
                nextParent = nextParent.parent;
            }

            RefreshChildren();
        }

        void RefreshChildren()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].DeterminePosition();
            }
        }

        public Transform()
        {
            children = new List<Transform>();
            childrenHash = new HashSet<int>();
            Position = Vector2.Zero;
        }

        public void Translate(float x, float y)
        {
            this.LocalPosition += new Vector2(x, y);
        }

        public void Translate(Vector2 change)
        {
            this.LocalPosition += change;
        }
    }
}
