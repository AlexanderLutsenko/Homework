package robots;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import robots.Graph;

public class GraphTest {
    
    public GraphTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
        Robots.readData("test.txt");
        graph = Robots.createGraph(); 
        nodes = graph.getNodes();
    }
    
    @After
    public void tearDown() {
    }

    @Test
    public void testGraph(){
        System.out.println("Graph");

        assertTrue(nodes[0].isConnectedWith(nodes[1]));
        assertTrue(nodes[0].isConnectedWith(nodes[4]));
        assertTrue(nodes[1].isConnectedWith(nodes[0]));
        assertTrue(nodes[1].isConnectedWith(nodes[2]));
        assertTrue(nodes[2].isConnectedWith(nodes[1]));
        assertTrue(nodes[2].isConnectedWith(nodes[3]));
        assertTrue(nodes[2].isConnectedWith(nodes[4]));
        assertTrue(nodes[3].isConnectedWith(nodes[2]));
        assertTrue(nodes[4].isConnectedWith(nodes[0]));
        assertTrue(nodes[4].isConnectedWith(nodes[2]));
        
        assertFalse(nodes[0].isConnectedWith(nodes[2]));
        assertFalse(nodes[4].isConnectedWith(nodes[3]));
        assertFalse(nodes[1].isConnectedWith(nodes[3]));
        
        assertEquals(nodes[0].getRobotCount(), 0);
        assertEquals(nodes[1].getRobotCount(), 1);
        assertEquals(nodes[2].getRobotCount(), 0);
        assertEquals(nodes[3].getRobotCount(), 0);
        assertEquals(nodes[4].getRobotCount(), 1);
    }
    
    /**
     * Test of draw method, of class Graph.
     */
    @Test
    public void testDraw() {
        System.out.println("Draw");
        graph.draw();
        
        assertFalse(nodes[0].isPainted());
        assertTrue(nodes[1].isPainted());
        assertFalse(nodes[2].isPainted());
        assertTrue(nodes[3].isPainted());
        assertTrue(nodes[4].isPainted());
    }

    /**
     * Test of isSolvable method, of class Graph.
     */
    @Test
    public void testIsSolvable() {
        System.out.println("isSolvable");
        assertTrue(graph.isSolvable());
    }

    private Graph graph;
    Node[] nodes;
}
