using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    [SelectionBase]
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField]
        private Context _context;
        
        private readonly Dictionary<Type, object> _tComponents = new();
        private readonly Dictionary<object, object> _idComponents = new();

        private void Reset()
        {
            _context = GetComponent<Context>();
        }
        
        public T Get<T>()
        {
            Type type = typeof(T);
            if (_tComponents.TryGetValue(type, out object obj)) 
                return (T) obj;

            T component = _context.Container.Resolve<T>();
            _tComponents.Add(type, component);
            return component;
        }

        public bool TryGet<T>(out T component) where T : class
        {
            Type type = typeof(T);
            if (_tComponents.TryGetValue(type, out object obj)) 
                component = (T) obj;

            component = _context.Container.TryResolve<T>();
            _tComponents.Add(type, component);
            return component != null;
        }

        public T Get<T>(object id)
        {
            if (_idComponents.TryGetValue(id, out object obj))
                return (T) obj;

            T component = _context.Container.ResolveId<T>(id);
            _idComponents.Add(id, component);
            return component;
        }

        public bool TryGet<T>(object id, out T component) where T : class
        {
            if (_idComponents.TryGetValue(id, out object obj))
                component = (T) obj;
            
            component = _context.Container.TryResolveId<T>(id);
            _idComponents.Add(id, component);
            return component != null;
        }
    }
}


