package network;

import java.io.*;
import java.util.*;

public class Network {

    public static void main(String[] args) {    
        
        securityRanks = new HashMap<>();                
        securityRanks.put("windows", 0.7f);
        securityRanks.put("macos", 0.3f);
        securityRanks.put("linux", 0.15f);

        ComputerNetwork net = createNetwork("input.txt");
        
        int infComp = (int) (Math.random() * compcount);
        net.infectComp(infComp);
        
        System.out.println("<Enter> - следующий шаг");
        Scanner in = new Scanner(System.in);

        while (!net.isDead()) {
            try {
                if (in.nextLine().equals("")) {
                    System.out.print(net.nextStep());
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static void readData(String filename) {
        File input = new File(filename);
        try {
            Scanner scan = new Scanner(input);
            String str = scan.nextLine();
            StringTokenizer tokenizer = new StringTokenizer(str);
            compcount = tokenizer.countTokens();
            OSystems = new String[compcount];
            adjmatrix = new boolean[compcount][compcount];
            for (int j = 0; j < compcount; j++) {
                OSystems[j] = tokenizer.nextToken().toLowerCase();
            }
            for (int j = 0; j < compcount; j++) {
                str = scan.nextLine();
                tokenizer = new StringTokenizer(str);
                for (int i = 0; i < compcount; i++) {
                    adjmatrix[i][j] = (convertToBoolean(Integer.parseInt(tokenizer.nextToken())));
                }
            }
            scan.close();
        } catch (FileNotFoundException e) {
            System.out.println("Файл не найден " + e);
            System.exit(0);
        } catch (Exception e) {
            System.out.println("Неверный формат ввода " + e);
            System.exit(0);
        }     
    }

    public static ComputerNetwork createNetwork(String source) {
        readData(source);
        return new ComputerNetwork(compcount, adjmatrix, OSystems, securityRanks);
    }

    private static boolean convertToBoolean(int number) {
        return number != 0;
    }
    private static String[] OSystems;
    private static boolean[][] adjmatrix;
    private static int compcount = 0;
    private static Map<String, Float> securityRanks;
}
