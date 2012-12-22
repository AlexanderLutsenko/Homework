package robots;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import robots.Graph;

/**
 *
 * @author Саня
 */
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
    }
    
    @After
    public void tearDown() {
    }

    /**
     * Test of draw method, of class Graph.
     */
    @Test
    public void testDraw() {
        System.out.println("Draw");
        graph.draw();
        Node[] nodes = graph.getNodes();
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
}
