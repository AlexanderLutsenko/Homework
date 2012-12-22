/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package robots;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Саня
 */
public class NodeTest {
    
    public NodeTest() {
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
     * Test of init method, of class Node.
     */
    @Test
    public void testInit() {
        System.out.println("init");        
        Node[] nodes = graph.getNodes();
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
    }

    private Graph graph;
}
