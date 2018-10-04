# UnityNpm - ExperimentalProject
Automatically generate submodules that can be added to Unity's package manager.  
Update and create the sub module branch corresponding to the Unity Package folder from the YAML definition file.  
If you have a repository etc. that you would like to add, it will be appreciated if you can pull request.  
  
We update each branch at world standard time 0 o'clock or when master is updated.  
Tools used for updating:[UnityNpmCI](https://github.com/KappaBull/UnityNpmCI)

## How to Use
git submodule add -b {CheckRepositoryName}/{Version} https://github.com/KappaBull/UnityNpm.git package/{PackageName}  
  
Example: Import Zenject ver7.1.0  
git submodule add -b Zenject/7.1.0 https://github.com/KappaBull/UnityNpm.git package/com.github.svermeulen.zenject  

## CheckRepository (MIT License Only)
### Framework
- [Zenject](https://github.com/svermeulen/Zenject)
- [UniRx](https://github.com/neuecc/UniRx.git)

### Util
- [Graphy](https://github.com/Tayx94/graphy)

### Effect
- [Pixel-Burn-Effect](https://github.com/Shealynntate/Pixel-Burn-Effect)

### Editor
- [unity-jar-resolver](https://github.com/googlesamples/unity-jar-resolver)