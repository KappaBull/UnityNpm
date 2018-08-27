﻿#if UNITY_EDITOR

using System;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using ModestTree;
using Assert=ModestTree.Assert;
using Zenject.Tests.Bindings.FromSubContainerPrefab;

#pragma warning disable 649

namespace Zenject.Tests.Bindings
{
    public class TestFromSubContainerPrefab : ZenjectIntegrationTestFixture
    {
        GameObject FooPrefab
        {
            get { return FixtureUtil.GetPrefab("TestFromSubContainerPrefab/Foo"); }
        }

        GameObject FooPrefab2
        {
            get { return FixtureUtil.GetPrefab("TestFromSubContainerPrefab/Foo2"); }
        }

        void CommonInstall()
        {
            Container.Settings = new ZenjectSettings(ValidationErrorResponses.Throw);
        }

        [UnityTest]
        public IEnumerator TestSelfSingle()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<Foo>().FromSubContainerResolve()
                .ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        [ValidateOnly]
        public IEnumerator TestSelfSingleValidate()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<Foo>().FromSubContainerResolve()
                .ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();
            yield break;
        }

        [UnityTest]
        [ValidateOnly]
        public IEnumerator TestSelfSingleValidateFails()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<Foo>().FromSubContainerResolve()
                .ByNewPrefab(FooPrefab2).AsSingle().NonLazy();

            Assert.Throws(() => PostInstall());
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfTransient()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<Foo>().FromSubContainerResolve().ByNewPrefab(FooPrefab).AsTransient().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfCached()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<Foo>().FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfSingleMultipleContracts()
        {
            PreInstall();
            CommonInstall();

            Container.Bind(typeof(Foo), typeof(Bar)).FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertComponentCount<Bar>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfCachedMultipleContracts()
        {
            PreInstall();
            CommonInstall();

            Container.Bind(typeof(Foo), typeof(Bar)).FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertComponentCount<Bar>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfTransientMultipleContracts()
        {
            PreInstall();
            CommonInstall();

            Container.Bind(typeof(Foo), typeof(Bar)).FromSubContainerResolve().ByNewPrefab(FooPrefab).AsTransient().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(2);
            FixtureUtil.AssertComponentCount<Foo>(2);
            FixtureUtil.AssertComponentCount<Bar>(2);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestConcreteSingle()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<IFoo>().To<Foo>().FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestConcreteTransient()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<IFoo>().To<Foo>().FromSubContainerResolve()
                .ByNewPrefab(FooPrefab).AsTransient().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestConcreteCached()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<IFoo>().To<Foo>().FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestConcreteSingleMultipleContracts()
        {
            PreInstall();
            CommonInstall();

            Container.Bind(typeof(Bar), typeof(IFoo)).To(typeof(Foo), typeof(Bar))
                .FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            FixtureUtil.AssertComponentCount<Bar>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestConcreteCachedMultipleContracts()
        {
            PreInstall();
            CommonInstall();

            Container.Bind(typeof(Foo), typeof(IFoo)).To<Foo>().FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            FixtureUtil.AssertComponentCount<Foo>(1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfIdentifiersFails()
        {
            PreInstall();
            CommonInstall();

            Container.Bind<Gorp>().FromSubContainerResolve().ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            Assert.Throws(() => PostInstall());
            yield break;
        }

        [UnityTest]
        public IEnumerator TestSelfIdentifiers()
        {
            PreInstall();
            CommonInstall();

            Container.Settings = new ZenjectSettings(ValidationErrorResponses.Throw);
            Container.Bind<Gorp>().FromSubContainerResolve("gorp").ByNewPrefab(FooPrefab).AsSingle().NonLazy();

            PostInstall();

            FixtureUtil.AssertNumGameObjects(1);
            yield break;
        }
    }
}

#endif
