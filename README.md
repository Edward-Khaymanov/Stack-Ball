# Stack Ball
[![Build project](https://github.com/Edward-Khaymanov/Stack-Ball/actions/workflows/main.yml/badge.svg)](https://github.com/Edward-Khaymanov/Stack-Ball/actions/workflows/main.yml)
___


Clone of the [Stack Ball](https://play.google.com/store/apps/details?id=com.azurgames.stackball) game

___
**Unity version**: `2021.3.6f1`

**Used plugins**:
- [Vibration](https://github.com/BenoitFreslon/Vibration)
- [Extenject](https://github.com/Mathijs-Bakker/Extenject)
___

**Navigation**:
- [Preview](#preview)
- [How to add more skins?](#how-to-add-more-skins)

## Preview

<p align="center">
	<img src ="https://user-images.githubusercontent.com/104985307/210295097-39c69efd-08e2-4818-82e1-ca84da97b744.gif" height="500">
	<img src ="https://user-images.githubusercontent.com/104985307/210384086-6be4f65d-4e09-4183-8fa8-11f0670a3af8.gif" height="500">
</p>

<details>
<summary><h2>How to add more skins?<h2></summary>
 
### STEP 1. Create material for your skin

:warning: YOU CAN SKIP THIS STEP AND PICK `DefaultBallSkin` IN NEXT STEP

1. Go to `\_Project\Materials\Skins\`
2. Create a new material and select `Custom/BallSkin` shader
3. Customize your material

![CreateMaterial](https://user-images.githubusercontent.com/104985307/210307277-4c48f4bb-465c-4e16-abaa-00ab94404b31.gif)

### STEP 2. CREATE AND SETUP SKIN

1. Go to `\_Project\Templates\Ball Skins\`
2. Right click => Create => Ball Skin
3. Сustomize your skin
	- **Skin** - pick mesh for your skin
	- **Material** - pick `DefaultBallSkin` if you dont create you own
	- **Store icon** - icon which represent your skin in store
	- **Use Material color** 
		- `true` - take color from material
		- `false` - take color from level color palette
	- **Is Unlocked**
		- `true` - you can pick this skin in store
		- `false` - you can't
	- **Store Order** - order in the store
4. Set Addresable checkbox enabled

![CreateSkin](https://user-images.githubusercontent.com/104985307/210375724-53fb4a53-0d26-40b3-8ff4-22738bd80ade.gif)

### STEP 3. SETUP ADDRESABLES

1. In the navigation bar, click Window => Asset Management => Addresables => Groups
2. Find your skin, right click => Simplify Addresable Names
3. In label column select `Ballskin`
4. Move skin to `BallSkins` group

![SetupAddresables](https://user-images.githubusercontent.com/104985307/210376311-3969fc3e-830c-46f5-82a6-795ea18cd45b.gif)

### STEP 4. Play

![Play](https://user-images.githubusercontent.com/104985307/210376543-6536b9d7-0e80-4e6d-b5bb-fea2760915bf.gif)

</details>

<details>
<summary><h2>How to add more color templates?<h2></summary>

1. Go to `_Project\Templates\Color Palettes`
2. Right click => Create => Color Palette
3. Customize:
	- **Main** - color for skin and trail, which use default material. Also progress bar color.
	- **Platform** - gradient for platforms. Sets colors from bottom to top.
	- **Background** - gradient for skybox. Sets colors from top to bottom.
4. Set Addresable checkbox enabled. Сonfigure to use Addresables as in [step 3 of adding the skin](#step-3-setup-addresables)

</details>

	
	
	
	
	
	
	
	
