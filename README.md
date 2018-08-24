# UnityNpm - ExperimentalProject
 Automatically generate submodules that can be added to Unity's package manager.  
YAMLの定義ファイルからUnityのPackageフォルダ対応のサブモジュール用ブランチを更新、作成します。  
追加して欲しいリポジトリ等ありましたらプルリクして頂けると助かります。  
  
世界標準時0時又はマスターが更新された際に各ブランチを更新します。  
更新用CIツール:[UnityNpmCI](https://github.com/KappaBull/UnityNpmCI)

## How to Use
git submodule add -b {CheckRepositoryName}/{Version} https://github.com/KappaBull/UnityNpm.git package/{PackageName}  
  
Example: Import Zenject ver7.1.0  
git submodule add -b Zenject/7.1.0 https://github.com/KappaBull/UnityNpm.git package/com.github.svermeulen.zenject  

## CheckRepository
- [Zenject](https://github.com/svermeulen/Zenject)
- [Graphy](https://github.com/Tayx94/graphy)
- [UniRx](https://github.com/neuecc/UniRx.git)
