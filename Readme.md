# How to use bricks of different types


<p>The following example demonstrates how to use bricks of different types. The code listed in this example creates a report with images (represented by <strong>ImageBrick</strong> objects) and text (represented by <strong>TextBrick</strong> objects). Every image displays a picture of a fish, and a text contains species characteristics and its description.</p>
<p>Also, this example demonstrates how to use <strong>PageImageBrick</strong> and <strong>PageInfoBrick</strong> objects to show additional information in a page header. For instance, the <strong>PageImageBrick</strong> displays a DevExpress logo at the top of every page, while two <strong>PageInfo</strong> bricks display different kinds of information (either the current date and time, or the page number), depending on the <strong>PageInfoTextBrick.PageInfo</strong> property value.</p>
<p>In addition, this example demonstrates how to add a <strong>CheckBoxBrick</strong> to a document, and how to use a simple <strong>Brick</strong> to draw double borders around other bricks.<br><br><strong>NOTE</strong><br>This example demonstrates an outdated approach to creating document sections by setting the <strong>BrickGraphics.Modifier</strong> property. Refer to <a href="https://www.devexpress.com/Support/Center/p/T335238">T335238: How to: Use Bricks of Different Types</a> to learn how to customize document sections using a printing link's dedicated events.</p>

<br/>


