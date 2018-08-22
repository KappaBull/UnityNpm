using System;
using System.Collections.Generic;
using ModestTree;
using System.Linq;

#if !NOT_UNITY3D
using UnityEngine;
#endif

namespace Zenject
{
    public class FromBinderGeneric<TContract> : FromBinder
    {
        public FromBinderGeneric(
            DiContainer bindContainer,
            BindInfo bindInfo,
            BindFinalizerWrapper finalizerWrapper)
            : base(bindContainer, bindInfo, finalizerWrapper)
        {
            BindingUtil.AssertIsDerivedFromTypes(typeof(TContract), BindInfo.ContractTypes);
        }

        // Shortcut for FromIFactory and also for backwards compatibility
        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromFactory<TFactory>()
            where TFactory : IFactory<TContract>
        {
            return FromIFactory(x => x.To<TFactory>().AsCached());
        }

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromIFactory(
            Action<ConcreteBinderGeneric<IFactory<TContract>>> factoryBindGenerator)
        {
            return FromIFactoryBase<TContract>(factoryBindGenerator);
        }

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod(Func<TContract> method)
        {
            return FromMethodBase<TContract>(ctx => method());
        }

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod(Func<InjectContext, TContract> method)
        {
            return FromMethodBase<TContract>(method);
        }

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethodMultiple(Func<InjectContext, IEnumerable<TContract>> method)
        {
            return FromMethodMultipleBase<TContract>(method);
        }

        public ScopeConditionCopyNonLazyBinder FromResolveGetter<TObj>(Func<TObj, TContract> method)
        {
            return FromResolveGetter<TObj>(null, method);
        }

        public ScopeConditionCopyNonLazyBinder FromResolveGetter<TObj>(object identifier, Func<TObj, TContract> method)
        {
            return FromResolveGetter<TObj>(identifier, method, InjectSources.Any);
        }

        public ScopeConditionCopyNonLazyBinder FromResolveGetter<TObj>(object identifier, Func<TObj, TContract> method, InjectSources source)
        {
            return FromResolveGetterBase<TObj, TContract>(identifier, method, source, false);
        }

        public ScopeConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(Func<TObj, TContract> method)
        {
            return FromResolveAllGetter<TObj>(null, method);
        }

        public ScopeConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(object identifier, Func<TObj, TContract> method)
        {
            return FromResolveAllGetter<TObj>(identifier, method, InjectSources.Any);
        }

        public ScopeConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(object identifier, Func<TObj, TContract> method, InjectSources source)
        {
            return FromResolveGetterBase<TObj, TContract>(identifier, method, source, true);
        }

        public ScopeConditionCopyNonLazyBinder FromInstance(TContract instance)
        {
            return FromInstanceBase(instance);
        }

#if !NOT_UNITY3D

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInChildren(
            Func<TContract, bool> predicate, bool includeInactive = false)
        {
            return FromComponentInChildren(false, predicate, includeInactive);
        }


        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInChildren(
            bool excludeSelf = false, Func<TContract, bool> predicate = null, bool includeInactive = false)
        {
            BindingUtil.AssertIsInterfaceOrComponent(AllParentTypes);

            return FromMethodMultiple((ctx) => {
                Assert.That(ctx.ObjectType.DerivesFromOrEqual<MonoBehaviour>());
                Assert.IsNotNull(ctx.ObjectInstance);

                var res = ((MonoBehaviour)ctx.ObjectInstance).GetComponentsInChildren<TContract>(includeInactive)
                    .Where(x => !ReferenceEquals(x, ctx.ObjectInstance));

                if (excludeSelf)
                {
                    res = res.Where(x => (x as Component).gameObject != (ctx.ObjectInstance as Component).gameObject);
                }

                if (predicate != null)
                {
                    res = res.Where(predicate);
                }

                return res;
            });
        }


        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInParents(bool excludeSelf = false, bool includeInactive = false)
        {
            BindingUtil.AssertIsInterfaceOrComponent(AllParentTypes);

            return FromMethodMultiple((ctx) =>
                {
                    Assert.That(ctx.ObjectType.DerivesFromOrEqual<MonoBehaviour>());
                    Assert.IsNotNull(ctx.ObjectInstance);

                    var res = ((MonoBehaviour)ctx.ObjectInstance).GetComponentsInParent<TContract>(includeInactive)
                        .Where(x => !ReferenceEquals(x, ctx.ObjectInstance));

                    if (excludeSelf)
                    {
                        res = res.Where(x => (x as Component).gameObject != (ctx.ObjectInstance as Component).gameObject);
                    }

                    return res;
                });
        }

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentSibling()
        {
            BindingUtil.AssertIsInterfaceOrComponent(AllParentTypes);

            return FromMethodMultiple((ctx) =>
                {
                    Assert.That(ctx.ObjectType.DerivesFromOrEqual<MonoBehaviour>(),
                        "Cannot use FromComponentSibling to inject data into non monobehaviours!");
                    Assert.IsNotNull(ctx.ObjectInstance);

                    return ((MonoBehaviour)ctx.ObjectInstance).GetComponents<TContract>()
                        .Where(x => !ReferenceEquals(x, ctx.ObjectInstance));
                });
        }

        public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInHierarchy(
            Func<TContract, bool> predicate = null, bool includeInactive = false)
        {
            BindingUtil.AssertIsInterfaceOrComponent(AllParentTypes);

            return FromMethodMultiple((ctx) => {
                var res = ctx.Container.Resolve<Context>().GetRootGameObjects()
                    .SelectMany(x => x.GetComponentsInChildren<TContract>(includeInactive))
                    .Where(x => !ReferenceEquals(x, ctx.ObjectInstance));

                if (predicate != null)
                {
                    res = res.Where(predicate);
                }

                return res;
            });
        }
#endif
    }
}
