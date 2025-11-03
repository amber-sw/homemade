<#import "template.ftl" as layout>
<@layout.registeredLayout displayMessage=true; section>
    <#if section = "form">
        <div id="kc-idp-link-confirm" class="space-y-4">
            <div class="text-center mb-6">
                <h2 class="text-2xl font-bold text-neutral-900 mb-2">${msg("confirmLinkIdpTitle")}</h2>
                <p class="text-neutral-600">${msg("confirmLinkIdpReviewProfile", idpDisplayName!"")}</p>
            </div>

            <form id="kc-idp-link-confirm-form" action="${url.loginAction}" method="post">
                <div class="space-y-3">
                    <button
                        type="submit"
                        class="w-full bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-4 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                        name="submitAction"
                        id="updateProfile"
                        value="updateProfile"
                    >
                        ${msg("confirmLinkIdpReviewProfile")}
                    </button>

                    <button
                        type="submit"
                        class="w-full bg-secondary-100 hover:bg-secondary-200 text-neutral-700 font-semibold py-2.5 px-4 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-secondary-500 focus:ring-offset-2"
                        name="submitAction"
                        id="linkAccount"
                        value="linkAccount"
                    >
                        ${msg("confirmLinkIdpContinue", idpDisplayName!"")}
                    </button>
                </div>
            </form>
        </div>
    </#if>
</@layout.registeredLayout>
