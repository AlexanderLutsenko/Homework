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
        tree = new BinSearchTree();
        tree.addElement(19);
        tree.addElement(4);
        tree.addElement(7);
        tree.addElement(7);
        tree.addElement(21);
    }

    @After
    public void tearDown() {
    }

    /**
     * Test of iterator method, of class BinSearchTree.
     */
    @Test
    public void testIterator() {
        System.out.println("iterator");
        BinSearchTree.TreeIterator iter = tree.iterator();
        assertEquals((int) iter.next(), 19);
        assertEquals((int) iter.next(), 4);
        assertEquals((int) iter.next(), 7);
        assertEquals((int) iter.next(), 7);
        assertEquals((int) iter.next(), 21);
    }

    /**
     * Test of addElement method, of class BinSearchTree.
     */
    @Test
    public void testAddElement() {
        System.out.println("addElement");
        BinSearchTree bstree = new BinSearchTree();
        assertFalse(bstree.findElement(8));
        bstree.addElement(8);
        assertTrue(bstree.findElement(8));
    }

    /**
     * Test of toLSF method, of class BinSearchTree.
     */
    @Test
    public void testToLSF() {
        System.out.println("toLSF");
        assertEquals(tree.toLSF(), "19(4(*,7(7,*)),21)");
    }
    private BinSearchTree tree;
}
