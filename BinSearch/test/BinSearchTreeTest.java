/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

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
public class BinSearchTreeTest {

    public BinSearchTreeTest() {
    }

    @BeforeClass
    public static void setUpClass() {
    }

    @AfterClass
    public static void tearDownClass() {
    }

    @Before
    public void setUp() {
    }

    @After
    public void tearDown() {
    }

    /**
     * Test of iterator method, of class BinSearchTree.
     */
    @Test
    public void testIterator() {
        BinSearchTree tree = new BinSearchTree();
        BinSearchTree.TreeIterator iter = tree.iterator();
    }

    /**
     * Test of addElement method, of class BinSearchTree.
     */
    @Test
    public void testAddElement() {
        BinSearchTree tree = new BinSearchTree();
        assertFalse(tree.findElement(19));
        tree.addElement(19);
        tree.addElement(4);
        tree.addElement(7);
        assertTrue(tree.findElement(19));
    }

    /**
     * Test of findElement method, of class BinSearchTree.
     */
    @Test
    public void testFindElement() {
        BinSearchTree tree = new BinSearchTree();
        assertFalse(tree.findElement(19));
        tree.addElement(19);
        tree.addElement(4);
        tree.addElement(7);
        assertTrue(tree.findElement(19));
    }

    /**
     * Test of toLSF method, of class BinSearchTree.
     */
    @Test
    public void testToLSF() {
        BinSearchTree tree = new BinSearchTree();
        tree.addElement(19);
        tree.addElement(4);
        tree.addElement(7);
        tree.addElement(7);
        tree.addElement(21);
        assertEquals(tree.toLSF(), "19(4(*,7(7,*)),21)");
    }
}
