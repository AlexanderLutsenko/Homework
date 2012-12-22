package network;

import java.util.HashMap;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

public class ComputerNetworkTest {

    public ComputerNetworkTest() {       
    }

    @BeforeClass
    public static void setUpClass() {
    }

    @AfterClass
    public static void tearDownClass() {
    }

    @Before
    public void setUp() {
        net = Network.createNetwork("test.txt");
        computers = net.getComputers();
        HashMap<String, Float> ranks = new HashMap<>();
        ranks.put("windows", 1f);
        ranks.put("macos", 1f);
        ranks.put("linux", 1f);
        
        net.setSecurityRanks(ranks);    
        net.infectComp(0);
    }

    @After
    public void tearDown() {
    }
    
    @Test
    public void testComputerNetwork() {
        System.out.println("ComputerNetwork");
        assertEquals(computers[0].getOS(), "linux");
        assertEquals(computers[1].getOS(), "windows");
        assertEquals(computers[2].getOS(), "macos");
        assertEquals(computers[3].getOS(), "macos");
        assertEquals(computers[4].getOS(), "linux");
        assertEquals(computers[5].getOS(), "linux");
        assertEquals(computers[6].getOS(), "windows");
        assertEquals(computers[7].getOS(), "windows");
    }
    
    @Test
    public void testNextStep() {
        System.out.println("nextStep");
        assertEquals(net.nextStep(), "Шаг 1:\n"
                + "    Компьютер 0 заразил компьютер 1(windows)\n"
                + "    Компьютер 0 заразил компьютер 3(macos)\n"
                + "    Компьютер 0 заразил компьютер 5(linux)\n"
                + "    Компьютер 0 заразил компьютер 7(windows)\n");
        assertTrue(computers[0].isInfected());
        assertTrue(computers[1].isInfected());
        assertTrue(computers[3].isInfected());
        assertTrue(computers[5].isInfected());
        assertTrue(computers[7].isInfected());
        assertFalse(computers[2].isInfected());
        assertFalse(computers[6].isInfected());
        
    }

    /**
     * Test of start method, of class ComputerNetwork.
     */
    @Test
    public void testStart() {
        System.out.println("start");
        assertEquals(net.nextStep(), "Шаг 1:\n"
                + "    Компьютер 0 заразил компьютер 1(windows)\n"
                + "    Компьютер 0 заразил компьютер 3(macos)\n"
                + "    Компьютер 0 заразил компьютер 5(linux)\n"
                + "    Компьютер 0 заразил компьютер 7(windows)\n");
        assertFalse(net.isDead());
        assertEquals(net.nextStep(), "Шаг 2:\n"
                + "    Компьютер 1 заразил компьютер 4(linux)\n"
                + "    Компьютер 3 заразил компьютер 6(windows)\n"
                + "    Компьютер 5 заразил компьютер 2(macos)\n");
        assertFalse(net.isDead());
        assertEquals(net.nextStep(), "Все компьютеры заражены!");
        assertTrue(net.isDead());
        assertEquals(net.nextStep(), "Все компьютеры заражены!");
        assertTrue(net.isDead());
    }
    private ComputerNetwork net;
    ComputerNetwork.Computer[] computers;
}
