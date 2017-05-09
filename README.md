# nuxeo-vr-hackathon

## Videos

[Nuxeo VR Hackathon Submission](https://youtu.be/4zkhtqIWQPs)

[Nuxeo VR Hackathon Submission (VR Mode)](https://youtu.be/QMxWmmBevdc)



## Google VR SDK Setup

1. Download [GoogleVRForUnityV1.10.0.unitypackage](https://drive.google.com/a/nuxeo.com/file/d/0B16BOhcXVOmgTFIza2FFMXRLN2c/view?usp=sharing) (for Unity 5.5.1f1)

2. Selecting the `Assets` folder, click `Assets` > `Import Package` > `Custom Package…` > `GoogleVRForUnityV1.10.0.unitypackage`

3. If an `API Update Required` alert pops up, click `I Made a Backup. Go Ahead!`

4. Open `Assets/GoogleVR/Scripts/Video/GvrVideoPlayerTexture.cs` and change line `595` to `yield return false;`

5. If an `Package Import Required` alert pops up, click `Import Package`



## iOS Development Setup

1. `File` > `Build Settings…`

2. `Add Open Scenes` > `iOS` > `Switch Platform`

3. `Player Settings…` > `Resolution and Presentation` > On `Allowed Orientations for Auto Rotation` disable everything but `Landscape Left`

4. `Player Settings…` > `Other Settings` > Set `Bundle Identifier` on `Identification`

5. `Build`
