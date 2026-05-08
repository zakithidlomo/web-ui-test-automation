Feature: Demo Web Shop - End-to-End Order Placement
  As a registered customer
  I want to purchase a custom computer from the Demo Web Shop
  So that I can complete an online order using Cash on Delivery

  Scenario: Place an order for Build your own cheap computer with Cash on Delivery
    Given I navigate to the Demo Web Shop website
    And I log in with registered credentials
    When I navigate to the Computers menu and select Desktops
    And I select the "Build your own cheap computer" product
    And I configure the product with default options
    And I add the product to the cart
    Then I should see a cart notification
    When I navigate to the shopping cart
    And I accept the terms of service
    And I proceed to checkout
    And I fill in the billing address details
    And I continue through the shipping address step
    And I select "Ground" as the shipping method
    And I select "Cash On Delivery" as the payment method
    And I continue through payment info
    And I confirm the order
    Then the order should be placed successfully
    And I capture and print the order number
