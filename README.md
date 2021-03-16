# gp_project_repo
 
Import Assets
1.	Add the sunny land asset from the asset store
2.	Download and import

Converting everything to 16-bit
1.	Select all tilesets and background images and make the ‘Pixels per unit’ 16.
2.	Apply the changes
Creating Tilesets
1.	Open an unsliced image.
2.	Open the Sprite Editor.
3.	Side option:
4.	Grid by Cell size
5.	16 x 16
6.	Slice
7.	Apply
8.	Aplly
9.	Create new Palette
10.	Drag and drop tileset to palette
11.	Save it and use it

Character Sprite
1.	Go to the character sprite
2.	Do converting to 16-bit
3.	Create a new GameObject called ‘Player’
4.	Add ‘Sprite Renderer’ component to it
5.	Add the idle to Sprite option
6.	Go to sorting layer
7.	Click on Add Sorting Layer
8.	Make ‘Base’, ‘Fore’, ‘Entity’
9.	Make changes to background by choosing ‘Back’
10.	Make changes to ground by choosing ‘fore’.
11.	Make changes to player by choosing ‘Entity’.

Adding colliders
1.	Added RigidBody 2D to ‘Player’
2.	Added BoxCollider 2D to ‘Player’
3.	Added TileMapCollider 2D to ‘Foreground’
4.	Added RigidBody 2D to ‘Foreground’
5.	Added CompositeCollider 2D to ‘Foreground’
6.	Checked the ‘Used By Composite’ box in Foreground’s RigidBody 2D





