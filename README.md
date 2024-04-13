# XYZ

XYZ (concept).

## Plot

XYZ.

## Project Structure

Game logic is located in [Assets/Shooter/Scripts](Assets/Shooter/Scripts) folder.

- [xyz.cs](Assets/Shooter/Scripts/xyz.cs) xyz.

Assets from Asset Store are located into a Store folder (Assets/Store/...)

## Documentation
### Player
#### Player cameras
Player cameras are located into Player preflab. There are two cameras. One for only weapon generation - WeaponCamera, second one for rest of layers - PlayerCamera.
It is important to add **new layers** into PlayerCamera, otherwise you won't be able to **see the new layers**. You can do this easly by:
Open Player preflab -> open PlayerBody -> click on PlayerCamera -> in inspector find "Camera" component -> find rendering -> click on Culling Mask and add your new layers. 


## Assets and resources

- [StarterAssets - FirstPerson](https://assetstore.unity.com/packages/essentials/starterassets-firstperson-updates-in-new-charactercontroller-pac-196525) is used for the player controls and camera.
- [Modern Weapons Pack](https://assetstore.unity.com/packages/3d/props/guns/modern-weapons-pack-14233) some of weapons models and textures.

## Contributors
Diana Meleshchenkova
- xyz

Hubert Zienda
- xyz

Kamil Szot
- Second camera for weapon texture rendering,

Szymon Sobieraj
- xyz

Piotr Grzegorzek
- xyz
