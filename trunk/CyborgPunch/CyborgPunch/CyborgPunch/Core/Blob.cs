using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace CyborgPunch.Core
{
    public class Blob
    {
        public int ID { get; private set; }
        public Transform transform;
        public bool enabled;

        HashSet<Type> componentHash;
        List<Component> components;

        private static int IDCount = 0;
        public Blob()
        {
            this.ID = IDCount++;
            BlobManager.Instance.RegisterBlob(this);

            this.enabled = true;
            this.components = new List<Component>();
            this.componentHash = new HashSet<Type>();

            this.transform = new Transform();
            AddComponent(transform);
        }

        public void AddComponent(Component component)
        {
            if (componentHash.Add(component.GetType()))
            {
                components.Add(component);
                component.blob = this;
                component.Start();
            }
        }

        public void RemoveComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                {
                    componentHash.Remove(typeof(T));
                    components.RemoveAt(i);
                    return;
                }
            }
        }

        public T GetComponent<T>() where T : Component
        {
            if (componentHash.Contains(typeof(T)))
            {
                for (int i = 0; i < components.Count; i++)
                {
                    if (components[i].GetType() == typeof(T))
                    {
                        return (T)components[i];
                    }
                }
            }

            return null;
        }

        public void Destroy()
        {
            BlobManager.Instance.UnregisterBlob(this);

            for (int i = 0; i < transform.Children.Count; i++)
            {
                transform.Children[i].blob.Destroy();
            }

        }

        public void Update()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].enabled)
                {
                    components[i].Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].enabled)
                {
                    components[i].Draw(spriteBatch);

                    if (Constants.DEBUG)
                    {
                        components[i].DrawDebug(spriteBatch);
                    }
                }
            }
        }

        public bool Collides(Blob other)
        {
            Collider myCollider = this.GetComponent<Collider>();
            Collider otherCollider = other.GetComponent<Collider>();
            if (myCollider == null || otherCollider == null)
            {
                return false;
            }
            else
            {
                return myCollider.Collides(otherCollider);
            }
        }
    }
}
