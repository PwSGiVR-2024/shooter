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
### API Client

To create an instance of the API class:
```csharp
API api = new API("http://your-api-url.com");
```
Methods belonging to this class are asynchronous, therefore calling method should be async too
```csharp
async void MyMethod()
{
    //...
}
```

In methods below, if the response status code indicates failure, it throws an APIException with the reason phrase from the response.

To register a new user:
```csharp
await api.Register("username", "password");
```
This method takes a username and password, creates a User object for JSON serialization, and sends a POST request to the "auth/login" endpoint.

To login a user:
```csharp
IEnumerable<string> cookies = await api.Login("username", "password");
```
This method takes a username and password, creates a User object for JSON serialization, and sends a POST request to the "auth/login" endpoint.
It returns an IEnumerable<string> representing the Set-Cookie headers from the response.

To authenticate a user with cookies:
```csharp
await api.Authenticate(cookies);
```
This method takes an IEnumerable<string> representing the Set-Cookie headers from a previous login response and sends a GET request to the "auth/authenticate" endpoint.

### Inventory and items system
Press f to interact with objects
Press c to open crafting panel
Press i to open inventory
All menus closes on clicking x icon, or when you open pause menu (esc), inventory can be closed by pressing i again, crafting closes when you move, vendor and lootbox closes when you are out of range
Items can be unstacked by holding shift and clicking on item.
Make sure to use tag Item container for game object holding item container script (used for closing them when pause menu is opened).
#### Adding new item
1. In Unity's top menu, navigate to Tools -> Devion Games -> Inventory System -> Editor.
2. In the Items tab, ensure that the chosen database on the left is "ShooterItemDatabase".
3. Press the '+' sign near All to add a usable item.
4. Provide the new item with a Name, Icon (64x64px), and Model.
5. Press 'setup' to create a prefab for the item.
6. Add a Description for the item, set the Stackability options for the item.
8. Define the Actions that occur when the item is used (press 'add action').
9. If the item is a single-use item, ensure to add a built-in 'remove item' action (and in item option choose item type).
#### Making a collectible item
Note: If collectible item falls of map try to increase model size or change to other type of collider.
1. Select the prefab of the item and add mesh to mesh collider (or replace it with sphere/box collider).
2. in Trigger script set user distance and remove left click from trigger type.
3. Choose ItemPickup Action Template
#### Making a sellable item
Dont set can buy back - its bugged
1. In Unity's top menu, navigate to Tools -> Devion Games -> Inventory System -> Editor.
2. In the Item Group tab choose Vendor group and add item to this group(if you want it in default vendor automatically).
3. In the Items tab, ensure that your item has checked "Is Sellable" option and set the price.
#### Making craftable item
1. In Unity's top menu, navigate to Tools -> Devion Games -> Inventory System -> Editor.
2. In the Item Group tab set is craftable to true
3. Crafting duration means how long it takes to craft item, ingredients are items needed to craft item.
#### Making custom action template
1. In InventorySystem/ActionTemplates folder, press right click -> Create -> Devion Games -> Triggers -> Action Template.
2. Make sure WindowName property is set to ItemContainer Name property (Inventory game object).
#### Model set up
1. Download obj/fbx with textures
2. Save model in Assets/Models, textures in Assets/Materials
3. Create new material and set texture to it (Base color, Roughness, Normal map)
4. Set material in model

#### Item container callbacks
Item container script from inventory and similiar prefabs provides callbacks (events executed on specific cases). In our case we utilize OnInventoryShow and OnInventoryHide events to prevent player from moving camera and shooting when inventory is open.
ItemContainerCallbacks.cs
```csharp
internal event Action<int> OnItemContainerCountChanged;
private int _itemContainerCount = 0;

public void OnItemContainerShow()
{
    _itemContainerCount++;
    OnItemContainerCountChanged?.Invoke(_itemContainerCount);
}

public void OnItemContainerHide()
{
    if (_itemContainerCount > 0)
    {
        _itemContainerCount--;
    }
    OnItemContainerCountChanged?.Invoke(_itemContainerCount);
}
```
We utilize observer design pattern to notify other classes about Item container reference count change. When callback is invoked, we call event passing item container count. Then classes handling this event can react accordingly.

Shooting.cs, here we prevent player from shooting when any item container is open.
```csharp
[SerializeField] ItemContainerCallbacks _itemContainerCallbacks;
private int _itemContainerCount;

private void Start()
{
    _itemContainerCallbacks.OnItemContainerCountChanged += UpdateItemContainerCount;
}

private void UpdateItemContainerCount(int itemContainerCount)
{
    _itemContainerCount = itemContainerCount;
}

private void OnLeftClick(InputAction.CallbackContext context)
{
    if (_itemContainerCount == 0)
    {
        // Can shoot
    }
}
```
First Person Controller, here we prevent player from moving camera when any inventory is open by saving and restoring rotation speed.
```csharp
[SerializeField] ItemContainerCallbacks _itemContainerCallbacks;
private float _savedRotationSpeed;

private void Start()
{
    _itemContainerCallbacks.OnItemContainerCountChanged += UpdateItemContainerCount;
}

private void UpdateItemContainerCount(int itemContainerCount)
{
    if (itemContainerCount == 1 && _savedRotationSpeed == 0)
    {
        _savedRotationSpeed = RotationSpeed;
        RotationSpeed = 0;
    }
    else if (itemContainerCount == 0)
    {
        RotationSpeed = _savedRotationSpeed;
        _savedRotationSpeed = 0;
    }
}
```

## Assets and resources
- [Item & Inventory System by Devion Games](https://assetstore.unity.com/packages/tools/gui/item-inventory-system-45568) used for inventory system, items, shop.
    - [Core concepts](https://deviongames.com/inventory-system/getting-started/core-concepts)
    - [How to setup library, create currency and create first collectible and usable item](https://youtu.be/bz1Gm-l1OXA?si=Lk8PfrSl1rD6AdY0)
    - [Custom actions](https://youtu.be/VPnWPQF1tWs?si=yLo6379q3y2j1NP0)
    - [Item containers](https://youtu.be/Zewsd8h-nkk?si=Wp3f5zKdGOmtcaoy)
    - [Lootboxes](https://youtu.be/FtWYRSa8n5o?si=hcwrKJB97YYoaY-A)
    - [Vendor](https://youtu.be/iqDIpVf9tkU?si=6YzMluuB22lvySNd)
    - [Crafting](https://youtu.be/PN73b-0rf0g?si=fJSaUw5kVrreClog)
- [Shooter API](https://github.com/piotr-grzegorzek/shooter_api) used for players authentication
- [Json & MessagePack Serialization](https://assetstore.unity.com/packages/p/json-messagepack-serialization-59918) for josn array serialization (built in library does not provide that)
- [StarterAssets - FirstPerson](https://assetstore.unity.com/packages/essentials/starterassets-firstperson-updates-in-new-charactercontroller-pac-196525) is used for the player controls and camera.
- [Modern Weapons Pack](https://assetstore.unity.com/packages/3d/props/guns/modern-weapons-pack-14233) some of weapons models and textures.

## Contributors
Diana Meleshchenkova - Product Owner:


Hubert Zienda - Developer:


Kamil Szot - Developer:
- Second camera for weapon texture rendering,


Szymon Sobieraj - Scrum Master:


Piotr Grzegorzek - Code KING:

