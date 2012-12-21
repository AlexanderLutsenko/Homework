package network;

import java.util.HashMap;
import java.util.Map;
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
public class ComputerNetworkTest {

    public ComputerNetworkTest() {
        net = Network.createNetwork("test.txt");
    }

    @BeforeClass
    public static void setUpClass() {
    }

    @AfterClass
    public static void tearDownClass() {
    }

    @Before
    public void setUp() {
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

    /**
     * Test of start method, of class ComputerNetwork.
     */
    @Test
    public void testStart() {
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
}
