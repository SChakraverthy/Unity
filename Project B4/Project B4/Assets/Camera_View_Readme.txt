///This assumes you have a brand new scene. If not, definitely use whatever textboxes you want


1. Create a 3D Object with buttons and levers and and screen and stuff on it

2. Tag that object "camera"

2. Create another 3D object that looks like a surveillance camera

3. Create a playable character

4. Add the "conversationv2", "playercontrollerv2", and "animator" components to the playable character (remove older components if necessary)

5. Add necessary character controller/animations

6. Drag the Main Camera that follows the playable character into the conversationv2 Main Camera slot

7. Create a Text UI; drag this into the conversationv2 Dialogue slot

8. Create another Camera; make this a parent of the 3D object with buttons

9. Start the game, walk over to the 3D object with buttons and levers, press I. You should now be looking through the surveillance camera. Press I again to leave.

